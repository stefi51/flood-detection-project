FROM node:12

WORKDIR /usr/src/app

COPY data-microservice/package*.json ./

RUN npm install

COPY data-microservice/ .

EXPOSE 3000

CMD [ "npm", "run", "dev" ]