import "./InventoryView.css"
import InventoryItem from "./InventoryItem";
import { useState } from 'react';

function InventoryView() {
	const [items, setItems] = useState([
		{ ProductName: "Bread", Count: 1, CountWarningThreshold: 3},
		{ ProductName: "Oatmeal", Count: 2, CountWarningThreshold: 1},
		{ ProductName: "Cereal", Count: 3, CountWarningThreshold: 2},
		{ ProductName: "Milk", Count: 4, CountWarningThreshold: 0},
		{ ProductName: "Eggs", Count: 1, CountWarningThreshold: 4 }
	]);
	items.sort((a, b) => a.Count > b.Count ? 1 : -1);

	const [searchText, setSearchText] = useState("");

	return (
		<>
			<div className="itemSearch">
				<input name="itemSearchField" type="search" onChange={e => setSearchText(e.target.value)}/>
			</div>
			<div className="inventoryItems">
				{ items 
					&& items.filter(item => item.ProductName.toLowerCase().indexOf(searchText.toLowerCase()) > -1).map(item => (
						<InventoryItem 
							itemName={item.ProductName} 
							itemStockAmount={item.Count}
							goodStockCount={item.CountWarningThreshold} />
					))}
			</div>
		</>
	);
}

export default InventoryView;
