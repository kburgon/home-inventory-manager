import { useState } from 'react';

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
		<form onSubmit={handleSubmit}>
			<div className="inputRow">
				<label className="inputLabel" >Item Number: </label>
				<input 
					type="text" 
					id="itemBarcodeInput" 
					name="itemBarcode"
					value={inputs.itemBarcode || ""}
					onChange={handleChange}
				/>
			</div>
			<div className="inputRow">
				<label className="inputLabel">Amount: </label>
				<input 
					type="text" 
					id="transactionAmountInput" 
					name="transactionAmount"
					value={inputs.transactionAmount || 0}
					onChange={handleChange}
				/>
			</div>
			<div className="inputRow">
				<input type="submit" name="addItems" value="Add Items" />
				<input type="submit" name="removeItems" value="Remove Items" />
			</div>
			<div className="msgRow">
				<p>{inputs.submitMsg}</p>
			</div>
		</form>
	);
}

export default ItemTransactionForm;
