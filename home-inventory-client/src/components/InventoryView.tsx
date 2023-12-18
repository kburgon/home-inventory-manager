import InventoryItem from "./InventoryItem";
function InventoryView() {
	return (
		<>
			<div className="itemSearch">
				<input name="itemSearchField" type="search" />
			</div>
			<div className="inventoryItems">
				<InventoryItem />
				<InventoryItem />
				<InventoryItem />
				<InventoryItem />
			</div>
		</>
	);
}

export default InventoryView;
