import { useEffect, useState } from 'react';
import Modal from 'react-modal';
import { Product } from './Models/Product';
import NewProductPopup from './NewProductPopup';

import "./ProductSelector.css";

interface ProductSelectorProps {
	onProductSelected: (productId: number) => void;
	onProductFetchResult: (success: boolean, message: string) => void;
}

function ProductSelector({ onProductSelected, onProductFetchResult }: ProductSelectorProps) {
	const [products, setProducts] = useState<Product[]>([]);
	const [isOpen, setIsOpen] = useState<boolean>(false);

	const getProducts = async () => {
		var productResults = await fetch('http://localhost:5223/api/products', {
			method: 'GET',
			headers: { 'Content-Type': 'application/json' }
		});

		console.log(productResults);
		switch (productResults.status) {
			case 400:
				var badRequestBody = await productResults.text();
			var response = JSON.parse(badRequestBody);
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

	const createProduct = async (product: Product) => {
		var result = await fetch('http://localhost:5223/api/products', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify(product)
		});

		switch (result.status) {
			case 400:
			case 500:
				var responseTxt = await result.text();
				var response = JSON.parse(responseTxt);
				console.log(response);
				break;
			default:
				await getProducts();
		}
	}

	const handleProductSelection = (event: React.ChangeEvent<HTMLSelectElement>) => {
		const productId = event.target.value;
		onProductSelected(Number(productId));
	}

	const onNewProductSubmitted = async (product: Product) => {
		await createProduct(product);
		console.log('New product name: ' + product.productName);
	}

	useEffect(() => {
		getProducts();
	});

	return (
		<>
			<select className='productSelect' onChange={handleProductSelection}>
				<option value="" key="">Select...</option>
			{
				products.map(product => (
					<option value={product.id} key={product.id}>{product.productName}</option>
				))
			}
			</select>
			<button className='openNewProductButton' onClick={() => setIsOpen(true)} >+</button>
			<Modal 
				isOpen={isOpen}
				onRequestClose={() => setIsOpen(false)}
				contentLabel="Test Modal"
				className="modal-content"
				overlayClassName="modal-overlay"
				>
				<NewProductPopup onNewProductSubmitted={onNewProductSubmitted} />
				<button onClick={() => setIsOpen(false)}>Close</button>
			</Modal>
		</>
	)
}

export default ProductSelector;
