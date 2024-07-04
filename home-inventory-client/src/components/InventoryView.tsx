import "./InventoryView.css"
import InventoryItem from "./InventoryItem";
import { useEffect, useState } from 'react';

function InventoryView() {
	const [items, setItems] = useState([
		{ productName: "Bread", count: 1, warningThreshold: 3},
		{ productName: "Oatmeal", count: 2, warningThreshold: 1},
		{ productName: "Cereal", count: 3, warningThreshold: 2},
		{ productName: "Milk", count: 4, warningThreshold: 0},
		{ productName: "Eggs", count: 1, warningThreshold: 4 }
	]);

	useEffect(() => {
		fetch('http://localhost:5223/api/products/all')
			.then((response) => response.json())
			.then((data) => {
				console.log(data);
				setItems(data);
			})
			.catch((err) => { console.log(err.message); });
	}, []);

	items.sort((a, b) => a.count > b.count ? 1 : -1);

	const [searchText, setSearchText] = useState("");

	return (
		<>
			<div className="itemSearch">
				<input name="itemSearchField" type="search" onChange={e => setSearchText(e.target.value)}/>
			</div>
			<div className="inventoryItems">
				{ items 
					&& items.filter(item => item.productName.toLowerCase().indexOf(searchText.toLowerCase()) > -1).map(item => (
						<InventoryItem 
							productName={item.productName} 
							itemStockAmount={item.count}
							goodStockCount={item.warningThreshold} />
					))}
			</div>
		</>
	);
}

export default InventoryView;
