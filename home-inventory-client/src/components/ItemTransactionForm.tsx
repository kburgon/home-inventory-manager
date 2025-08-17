import { useState } from 'react';
// import { Result } from '@zxing/library';
import "./ItemTransactionForm.css";
import { Product } from './Models/Product';
import ProductSelector from './ProductSelector';

function ItemTransactionForm() {
	const [productId, setProductId] = useState(0);
	const [count, setCount] = useState(0);
	const [submitMsg, setSubmitMsg] = useState("");

	const handleSubmit = async (event:any) => {
		try {
			event.preventDefault();
			var product: Product = { id: 0, productName: '', count: 0, warningThreshold: 0 };
			var productResult = await fetch(`http://localhost:5223/api/products/${productId}`, {
				method: 'GET',
				headers: { 'Content-Type': 'application/json'}
			});

			if (productResult.status === 400) {
				var message = (await productResult.json()).message;
				setSubmitMsg(`Bad request: ${message}`);
				return;
			}

			product = (await productResult.json()) as Product;
			console.log(`Found product name: ${product.productName}`);
			if (product != null && product.id !== productId) {
				console.log('unable to fetch ProductId');
				return;
			}

			const submitter = event.nativeEvent.submitter.name;
			var adjustment = submitter === 'removeItems' ? count * -1 : count;
			console.log(`Submitted adjustment amount: ${adjustment}`);
			product.count += adjustment;

			var requestOptions = {
				method: 'PUT',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify(product)
			}

			var adjustResult = await fetch('http://localhost:5223/api/products', requestOptions);
			if (adjustResult.status === 200) {
				setSubmitMsg(`Stock of ${product.productName} adjusted by ${adjustment}.`);
			}
			else {
				setSubmitMsg(`Response returned with status code ${adjustResult.status}`);
			}
		}
		catch (error)
		{
			setSubmitMsg(`Error encountered: ${error}`);
		}
	}

	const scrubNum = (value: string) => {
		const result = value.replace(/\D/g, '');
		if (result === '') {
			return '0';
		}

		return result;
	}
	
    const onProductFetchResult = (success: boolean, message: string) => {
		setSubmitMsg(`Product fetch success: ${success}, Message: ${message}`);
    }

	const onProductSelected = (productId: number) => {
		setProductId(productId);
		console.log(`Set product ID: ${productId}`);
	}

	// const handleScanBarcode = (result: Result) => {
	// 	setInputs(values => ({...values, itemBarcode:result.toString()}));
	// }
	//
	// const handleScanBarcodeError = (error: Error) => {
	// 	setInputs(values => ({...values, setScanResult:error.message}));
	// }

	return (
		<>
			<form onSubmit={handleSubmit}>
				<div className='inputRow'>
					<label className='inputColumn1' >Product: </label>
					<ProductSelector 
						onProductFetchResult={onProductFetchResult} 
						onProductSelected={onProductSelected} 
					/>
				</div>
				<div className="inputRow">
					<label className="inputColumn1">Amount: </label>
					<input 
						className="inputColumn2"
						type="number" 
						id="countInput" 
						name="count"
						defaultValue={count}
						onChange={event => {setCount(parseInt(scrubNum(event.target.value)))}}
						min="1"
						step="1"
					/>
				</div>
				<div className="inputRow">
					<input className="transSubmit" type="submit" name="addItems" value="Add Items" />
					<input className="transSubmit" type="submit" name="removeItems" value="Remove Items" />
				</div>
				<div className="msgRow">
					<p>{submitMsg}</p>
				</div>
			</form>
		</>
	);
}

export default ItemTransactionForm;
