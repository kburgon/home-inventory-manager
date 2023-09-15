import React from 'react';
import ReactDOM from 'react-dom/client';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import './index.css';
import App from './App';
import InventoryHome from "./routes/InventoryHome";
import AddUseItems from "./routes/AddUseItems";
import reportWebVitals from './reportWebVitals';
import { ApolloClient, HttpLink, InMemoryCache, ApolloProvider } from '@apollo/client';

const router = createBrowserRouter([
	{
		path: "/",
		element: <App />,
		children: [
			{
				path: "/",
				element: <InventoryHome />
			},
			{
				path: "/adduseitems",
				element: <AddUseItems />
			}
		]
	}
]);

const gqlClient = new ApolloClient({
	cache: new InMemoryCache(),
	link: new HttpLink({
		fetchOptions:{
			mode: 'no-cors'
		},
		uri: 'https://192.168.1.12:44350/graphql'
	}),
});

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
	  <ApolloProvider client={gqlClient}>
		  <RouterProvider router={router} />
	  </ApolloProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
