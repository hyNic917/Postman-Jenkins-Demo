const newman = require('newman');

module.exports.runNewman = function () {
  console.log('hi');
  newman.run({
    collection: require('../Petstore.postman_collection.json'),
    reporters: ['cli', 'json', 'htmlextra', 'csv']
}, function (err) {
	if (err) { throw err; }
    console.log('collection run complete!');
})
.on('beforeRequest', function (err, o) {
    console.log(o.item.name);	
})
.on('assertion', (err, e) => {
    //const { assertion } = e
    console.log(e.assertion)
    const key = err ? 'failed' : e.skipped ? 'skipped' : 'executed'
    console.log(e);

    // log[key] = log[key] || []
    // log[key].push(assertion)
});
// .on("beforeDone", function() {
//     var report = _.get(newman, "summary.run.executions"),
//         collection = _.get(newman, "summary.collection"),
//         cache,
//         collName;

//     console.log(report);
// });
};