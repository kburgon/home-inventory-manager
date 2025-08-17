import { useEffect, useState } from 'react';
import { Product } from './Models/Product';

import "./ProductSelector.css";

interface ProductSelectorProps {
	onProductSelected: (productId: number) => void;
	onProductFetchResult: (success: boolean, message: string) => void;
}

function ProductSelector({ onProductSelected, onProductFetchResult }: ProductSelectorProps) {
	const [products, setProducts] = useState<Product[]>([]);
	const newProductSelector: string = "new";

	useEffect(() => {
		const getProducts = async () => {
			var productResults = await fetch('http://localhost:5223/api/products', {
				method: 'GET',
				headers: { 'Content-Type': 'application/json' }
			});

			console.log(productResults);
			switch (productResults.status) {
				case 400:
					var responseTxt = await productResults.text();
					var response = JSON.parse(responseTxt);
					onProductFetchResult(false, response.message);
					break;
				case 404:
					onProductFetchResult(false, response.message);
					break;
				case 500:
					onProductFetchResult(false, response.message);
					break;
				default:
					var responseTxt = await productResults.text();
					var p = JSON.parse(responseTxt);
					onProductFetchResult(true, '');
					setProducts(p);
			}
		}

		getProducts();
	}, []);

	const handleProductSelection = (event: React.ChangeEvent<HTMLSelectElement>) => {
		const productId = event.target.value;
		if (productId === newProductSelector) {
			console.log("New product selected");
			onProductSelected(0);
			return;
		}

		onProductSelected(Number(productId));
	}

	return (
		<>
			<select className='productSelect' onChange={handleProductSelection}>
			{
				products.map(product => (
					<option value={product.id} key={product.id}>{product.productName}</option>
				))
			}
				<option value={newProductSelector} key="0">New...</option>
			</select>
		</>
	)
}

export default ProductSelector;
