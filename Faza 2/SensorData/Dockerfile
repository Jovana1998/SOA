FROM node:14
WORKDIR /app
COPY package*.json app.js aa.txt ./ 
RUN npm install 
EXPOSE 3000
CMD ["node", "app.js"]