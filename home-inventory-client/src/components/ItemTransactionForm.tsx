import { useState } from 'react';
import { useQuery, gql } from '@apollo/client';
import { BarcodeScanner } from './BarcodeScanner';
import { Result } from '@zxing/library';
import "./ItemTransactionForm.css";

const PRODUCTS_QUERY = gql`
	query {
	  allProducts {
		productName
	  }
	}`;

function GetProducts() {
	const { loading, error, data } = useQuery(PRODUCTS_QUERY);

	if (loading) return "loading";
	if (error) return error.message;

	return data.locations;
}

function ItemTransactionForm() {
	const [inputs, setInputs] = useState(
		{
			itemBarcode : "", 
			transactionAmount : 0.0,
			submitMsg: "",
			scanResult: ""
		});

	const handleChange = (event:any) => {
		const name = event.target.name;
		const value = event.target.value;
		setInputs(values => ({...values, [name]:value}));
	};

	const handleSubmit = (event:any) => {
		event.preventDefault();
		const submitter = event.nativeEvent.submitter.name;
		// TODO: Send graphql call to save items
		setInputs(values => ({...values, submitMsg:submitter}));
	}
	
	const handleScanBarcode = (result: Result) => {
		setInputs(values => ({...values, itemBarcode:result.toString()}));
	}

	const handleScanBarcodeError = (error: Error) => {
		setInputs(values => ({...values, setScanResult:error.message}));
	}

	return (
		<>
			<form onSubmit={handleSubmit}>
				<div className="scanner">
					<BarcodeScanner onResult={handleScanBarcode} onError={handleScanBarcodeError} />
					<span>{inputs.scanResult}</span>
				</div>
				<div className="inputRow">
					<label className="inputColumn1" >Item Number: </label>
					<input 
						className="inputColumn2"
						type="text" 
						id="itemBarcodeInput" 
						name="itemBarcode"
						value={inputs.itemBarcode || ""}
						onChange={handleChange}
					/>
				</div>
				<div className="inputRow">
					<label className="inputColumn1">Amount: </label>
					<input 
						className="inputColumn2"
						type="number" 
						id="transactionAmountInput" 
						name="transactionAmount"
						value={inputs.transactionAmount || 0}
						onChange={handleChange}
						min="1"
						step="1"
					/>
				</div>
				<div className="inputRow">
					<input className="transSubmit" type="submit" name="addItems" value="Add Items" />
					<input className="transSubmit" type="submit" name="removeItems" value="Remove Items" />
				</div>
				<div className="msgRow">
					<p>{inputs.submitMsg}</p>
				</div>
			</form>
			<p>{GetProducts()}</p>
		</>
	);
}

export default ItemTransactionForm;
