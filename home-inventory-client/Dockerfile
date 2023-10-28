FROM node:20.9.0-alpine3.18
WORKDIR /home-inventory-client/
COPY home-inventory-client/public/ /home-inventory-client/public
COPY home-inventory-client/src/ /home-inventory-client/src
COPY home-inventory-client/package.json /home-inventory-client/
COPY home-inventory-client/tsconfig.json /home-inventory-client/

RUN npm install
CMD ["npm", "start"]
