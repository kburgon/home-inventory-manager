import { useState } from "react";
import { Product } from "./Models/Product";

import './NewProductPopup.css';

interface NewProductPopupProps {
	onNewProductSubmitted: (product: Product) => void;
	onClosed: () => void;
}

function NewProductPopup({ onNewProductSubmitted, onClosed }: NewProductPopupProps) {
	var [productName, setProductName] = useState("");
	var [warningThreshold, setWarningThreshold] = useState(0);

	const submitNewProduct = () => {
		const product: Product = {
			id: 0,
			productName: productName,
			warningThreshold: warningThreshold,
			count: 0
		};
		onNewProductSubmitted(product);
	}

	return (
		<>
			<label>Product Name:</label>
			<input 
				id="productName" 
				type="text" 
				defaultValue={productName} 
				onChange={event => setProductName(event.target.value)} 
				/>
			<label>Warning Threshold:</label>
			<input 
				id="warningThreshold" 
				type="number" 
				defaultValue={warningThreshold} 
				onChange={event => setWarningThreshold(parseInt(event.target.value))} 
				/>
			<div className="display-actions">
				<button className="btnAction" value="Save" onClick={() => submitNewProduct()}>Save</button>
				<button className="btnAction" value="Close" onClick={() => onClosed()}>Close</button>
			</div>
		</>
	)
}

export default NewProductPopup;
