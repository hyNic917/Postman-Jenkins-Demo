const newman = require('newman');

module.exports.runNewman = function () {
  console.log('hi');
  newman.run({
    collection: require('../Petstore.postman_collection.json'),
    reporters: ['cli', 'json', 'htmlextra', 'newman-reporter-csv']
}, function (err) {
	if (err) { throw err; }
    console.log('collection run complete!');
})
.on('beforeRequest', function (err, o) {
    console.log(o.item.name);	
});
// .on("beforeDone", function() {
//     var report = _.get(newman, "summary.run.executions"),
//         collection = _.get(newman, "summary.collection"),
//         cache,
//         collName;

//     console.log(report);
// });
};