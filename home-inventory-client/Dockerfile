FROM node:20.9.0-alpine3.18
WORKDIR /home-inventory-client/
COPY public/ /home-inventory-client/public
COPY src/ /home-inventory-client/src
COPY package.json /home-inventory-client/
COPY tsconfig.json /home-inventory-client/

RUN npm install
CMD ["npm", "start"]
