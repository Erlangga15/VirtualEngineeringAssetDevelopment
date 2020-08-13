FROM node:lts-alpine

WORKDIR /home/erlangga/Documents/project

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 8000

CMD [ "node", "server.js" ]

