const mongoose = require('mongoose');



const dataSchema = new mongoose.Schema({
    _id: {
        type: String,
        required: true
    },

    temperature_c: {
        type: String,
        required: true
    },

    humidity_p: {
        type: String,
        required: true
    },

    ping_ms: {
        type: String,
        required: true
    },

    time_id: {
        type: String,
        required: true
    },

});


const Data = mongoose.model("Data", dataSchema);

module.exports = { Data };
