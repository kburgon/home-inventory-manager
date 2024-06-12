import "./InventoryView.css"
import InventoryItem from "./InventoryItem";
import { useState } from 'react';

function InventoryView() {
	const [items, setItems] = useState([
		{ itemName: "Bread", stock: 1, warningThreshold: 3},
		{ itemName: "Oatmeal", stock: 2, warningThreshold: 1},
		{ itemName: "Cereal", stock: 3, warningThreshold: 2},
		{ itemName: "Milk", stock: 4, warningThreshold: 0},
		{ itemName: "Eggs", stock: 1, warningThreshold: 4 }
	]);
	items.sort((a, b) => a.stock > b.stock ? 1 : -1);

	const [searchText, setSearchText] = useState("");

	return (
		<>
			<div className="itemSearch">
				<input name="itemSearchField" type="search" onChange={e => setSearchText(e.target.value)}/>
			</div>
			<div className="inventoryItems">
				{ items 
					&& items.filter(item => item.itemName.toLowerCase().indexOf(searchText.toLowerCase()) > -1).map(item => (
						<InventoryItem 
							itemName={item.itemName} 
							itemStockAmount={item.stock}
							goodStockCount={item.warningThreshold} />
					))}
			</div>
		</>
	);
}

export default InventoryView;
