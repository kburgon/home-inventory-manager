import "./InventoryItem.css";

function InventoryItem() {
	return (
		<div className="inventoryItem">
			<div className="itemTitle">
				<div className="productName">Oatmeal</div>
				<div className="barcode">4wF33225</div>
			</div>
			<div className="itemStock">
				<span className="stockAmount">7</span>
			</div>
		</div>
	);
}

export default InventoryItem;
