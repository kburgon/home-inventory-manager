import { useState } from 'react';

function ItemTransactionForm() {
	const [inputs, setInputs] = useState({});

	const handleChange = (event) => {
		const name = event.target.name;
		const value = event.target.value;
		setInputs(values => ({...values, [name]:value}));
	};

	// Continue following here: https://www.w3schools.com/react/react_forms.asp

	return (
		<form>
			<div className="inputRow">
				<label className="inputLabel" >Item Number: </label>
				<input type="text" id="itemBarcodeInput" />
			</div>
			<div className="inputRow">
				<label className="inputLabel">Amount: </label>
				<input type="text" id="transactionAmountInput" />
			</div>
			<div className="inputRow">
				<input type="button" value="Add Items" />
				<input type="button" value="Remove Items" />
			</div>
		</form>
	);
}

export default ItemTransactionForm;
