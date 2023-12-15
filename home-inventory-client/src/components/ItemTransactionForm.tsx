import { useState } from 'react';
import { useQuery, gql } from '@apollo/client';
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
			submitMsg: ""
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

	return (
		<>
			<form onSubmit={handleSubmit}>
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
