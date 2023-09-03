import { useState } from 'react';

function ItemTransactionForm() {
	const [inputs, setInputs] = useState({itemBarcode : "", transactionAmount : 0.0});

	const handleChange = (event:any) => {
		const name = event.target.name;
		const value = event.target.value;
		setInputs(values => ({...values, [name]:value}));
	};

	const handleSubmit = (event:any) => {
		event.preventDefault();
		const submitter = event.nativeEvent.submitter.name;
		if (submitter === "addItems") {
			console.log("adding items");
		}
		else {
			console.log("removing items");
		}
	}

	// Continue following here: https://www.w3schools.com/react/react_forms.asp

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
		</form>
	);
}

export default ItemTransactionForm;
