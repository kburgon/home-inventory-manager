import InventoryItem from "./InventoryItem";
import { useState } from 'react';

function InventoryView() {
	const [items, setItems] = useState([
		{ itemName: "Bread", stock: 1},
		{ itemName: "Oatmeal", stock: 2},
		{ itemName: "Cereal", stock: 3},
		{ itemName: "Milk", stock: 4}
	]);

	return (
		<>
			<div className="itemSearch">
				<input name="itemSearchField" type="search" />
			</div>
			<div className="inventoryItems">
				{ items 
					&& items.map(item => (
						<InventoryItem itemName={item.itemName} itemStockAmount={item.stock} />
					))}
			</div>
		</>
	);
}

export default InventoryView;
