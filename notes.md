# Home Inventory

A solution to keeping track of the storage in your home

## Functional Requirements

1. I can keep a current count of all of the non-perishable food items that I have at home
2. I can look up a food item to see how much of that food item I have in stock
3. When I purchase groceries, I can scan them in to increment the quantity of the item in inventory
4. When I use an item, I can scan it in to decrement the quantity of the item in inventory
5. I can "add" a cooklang recipe's items to a shopping list
6. I can share the shopping list via email

## High-Level Design

### Central Pieces

- Database, data access API (graphql)
- User SPA, web application (mobile)
- Barcode processor (API)
- Inventory Manager (web service API)

## Data-Access API

### Functional Requirements

1. Can query to get a count of a collection of items
2. Can query to CRUD an item count
3. Can query to CRUD an item

### Non-Functional Requirements

- PostgreSQL as the DB?
- ASP.NET webapi and GraphQL.NET
- Hosted in two containers

## Inventory Manager API
### Functional Requirements

1. Can perform main business logic of actions
2. Offer the ability to undo actions
3. Answer the following API requests
	a. Scan (Insert) new item
	b. Create new item type
	c. Decrement quantity of an item

### Non-Functional Requirements

1. Workflow-type pattern

