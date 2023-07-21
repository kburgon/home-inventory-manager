import React from 'react';

const ItemTransactionForm = () => {
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
