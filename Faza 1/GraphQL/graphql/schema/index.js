const { buildSchema } = require('graphql')


module.exports = buildSchema(`

  type Data {
    time_id: String!
    ping_ms: String!
    temperature_c: String!
    humidity_p: String!
  }
  
  
  input DataInput {
    time_id: String!
    ping_ms: String!
    temperature_c: String!
    humidity_p: String!
  }
  input UpdateDataInput {
    id: String!
    time_id: String!
    ping_ms: String!
    temperature_c: String!
    humidity_p: String!
  }
  
  type Query {
    allData:[Data!]
    getByIdData(id:String!):Data!
  }

  type Mutation {
    createData(data:DataInput!): Data
    updateData(data:UpdateDataInput!): Data
    deleteData(id:String!): Data
  }

  schema {
    query: Query
    mutation: Mutation
  }
`)