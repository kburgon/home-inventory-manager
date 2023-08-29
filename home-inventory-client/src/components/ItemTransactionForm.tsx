import React from 'react';

class ItemTransactionForm extends React.Component {
	constructor(props:any) {
		super(props);
		this.state = {
			itemNumber: '',
			amount: 0.0
		};

		this.handleInputChange = this.handleInputChange.bind(this);
	}

	handleInputChange(event:any) {
		const target = event.target;
		const value = target.type === 'checkbox' ? target.checked : target.value;
		const name = target.name;

		this.setState({
			[name]: value
		});
	}

	render() {
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
}

export default ItemTransactionForm;
