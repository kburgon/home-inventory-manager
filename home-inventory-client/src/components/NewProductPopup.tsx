import { warning } from "@remix-run/router/dist/history";
import { useState } from "react";
import { Product } from "./Models/Product";
import ProductSelector from "./ProductSelector";

interface NewProductPopupProps {
	onNewProductSubmitted: (product: Product) => void;
}

function NewProductPopup({ onNewProductSubmitted }: NewProductPopupProps) {
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
			<button value="Save" onClick={() => submitNewProduct()}>Save</button>
		</>
	)
}

export default NewProductPopup;
