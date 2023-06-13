const express = require("express");
const mongoose = require("mongoose");

const graphqlHttp = require('express-graphql').graphqlHTTP;
const graphqlSchema = require('./graphql/schema');
const graphqlResolvers = require('./graphql/resolvers');
const expressPlayground = require("graphql-playground-middleware-express")
  .default;
const { Data } = require("./models");

const app = express();

app.use(express.json());

app .get("/playground", expressPlayground({ endpoint: "/graphql" })); // Use GraphQL Playground
app.use('/graphql', graphqlHttp({
    schema:graphqlSchema,
    rootValue:graphqlResolvers,
    graphiql: true
}));

const start = async () => {
  try {
    await mongoose.connect(
      "mongodb+srv://soa:soa12345@cluster0.xnw0z.mongodb.net/?retryWrites=true&w=majority"
    );
    app.listen(3000, () => console.log("Server started on port 3000"));
  } catch (error) {
    console.error(error);
    process.exit(1);
  }
};

start();