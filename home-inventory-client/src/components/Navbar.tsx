import React from 'react';
import { Link } from "react-router-dom";
import './Navbar.css';

const Navbar = () =>{
	return (
		<ul>
			<li>
				<Link to="/">Inventory Home</Link>
			</li>
			<li>
				<Link to="/adduseitems">Add/Use Items</Link>
			</li>
		</ul>
	);
}

export default Navbar;
