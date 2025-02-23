import { count } from 'console';
import { useRef, useState } from 'react';
// import { Result } from '@zxing/library';
import "./ItemTransactionForm.css";

function ItemTransactionForm() {
	const [productId, setProductId] = useState(0);
	const [count, setCount] = useState(0);
	const [submitMsg, setSubmitMsg] = useState("");

	const handleSubmit = (event:any) => {
		event.preventDefault();
		const submitter = event.nativeEvent.submitter.name;
		var adjustment = submitter === 'removeItems' ? count * -1 : count;
		console.log('Submitted adjustment amount: ' + adjustment);
		console.log('productId type' + typeof(productId));
		var requestOptions = {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({ productId: productId, stockAdjustment: count })
		}

		fetch('http://localhost:5223/api/adjustStock', requestOptions)
		.then(response => {
			if (response.ok)
				setSubmitMsg('Stock of ' + productId + ' adjusted by ' + adjustment.toString() + '.');
			else
				setSubmitMsg('Response returned with status code ' + response.status.toString());
		})
		.catch(error => {
			console.log(error.message);
			console.log(error.method);
			setSubmitMsg('ERROR: ' + error.message);
		})
		setSubmitMsg(submitter + ' ' + count + ' for ' + productId);
	}

	const scrubNum = (value: string) => {
		const result = value.replace(/\D/g, '');
		if (result === '') {
			return '0';
		}

		return result;
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
				<div className="inputRow">
					<label className="inputColumn1" >Product ID: </label>
					<input 
						className="inputColumn2"
						type="text" 
						id="productIdInput" 
						name="productId"
						defaultValue={productId}
						onChange={event => {setProductId(parseInt(scrubNum(event.target.value)))}}
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
