const {Data}= require('../../models')

module.exports = {
        allData: () => {
          return Data.find({})
            .then(datas => {
              return datas.map(event => {
                return { ...event._doc, _id: event.id };
              });
            })
            .catch(err => {
              throw err;
            });
        },
        createData: args => {
            console.log(args);
          const event = new Data({
            temperature_c: args.data.temperature_c,
            humidity_p: args.data.humidity_p,
            ping_ms: args.data.ping_ms,
            time_id: new Date(args.data.time_id)
          });
          return event
            .save()
            .then(result => {
              console.log(result);
              return { ...result._doc, _id: result._doc._id.toString() };
            })
            .catch(err => {
              console.log(err);
              throw err;
            });
        },
        updateData: args => {
            console.log(args);
         
            var temperature_c=args.data.temperature_c;
            var humidity_p=args.data.humidity_p;
            var ping_ms= args.data.ping_ms;
            var time_id= new Date(args.data.time_id);
          return Data.findByIdAndUpdate(args.data.id, {temperature_c,humidity_p, ping_ms, time_id})
            .then(result => {
              console.log(result);
              return { ...result._doc, _id: result._doc._id.toString() };
            })
            .catch(err => {
              console.log(err);
              throw err;
            });
        },
        getByIdData: args => {
            console.log(args);
          
          return Data.findById(args.id)
            .then(result => {
              console.log(result);
              return { ...result._doc, _id: result._doc._id.toString() };
            })
            .catch(err => {
              console.log(err);
              throw err;
            });
        },
        deleteData: args => {
            console.log(args);
           return Data.findByIdAndDelete(args.id)
            .then(result => {
              console.log(result);
              return { ...result._doc, _id: result._doc._id.toString() };
            })
            .catch(err => {
              console.log(err);
              throw err;
            });
        }
    
 
}

