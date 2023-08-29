import React from 'react';
import { Outlet } from "react-router-dom";
import Navbar from "./components/Navbar";
import './App.css';

class App extends React.Component {
	public render() {
		return (
			<div className="App">
				<Outlet />
				<div className="footer">
					<Navbar />
				</div>
			</div>
		);
	}
}

export default App;
