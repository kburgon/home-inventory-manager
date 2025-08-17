import { useState } from "react";

function NewProductPopup() {
	var [productName, setProductName] = useState("");
	var [warningThreshold, setWarningThreshold] = useState(0);

	return (
		<>
			<label>Product Name:</label>
			<input type="text" />
			<label>Warning Threshold:</label>
			<input type="number" />
			<button value="Save" >Save</button>
		</>
	)
}

export default NewProductPopup;
