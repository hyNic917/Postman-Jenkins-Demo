const newman = require('newman');
module.exports.init = function () {
  console.log('hi');
  newman.run({
    collection: require('./sample-collection.json'),
    reporters: 'cli'
}, function (err) {
	if (err) { throw err; }
    console.log('collection run complete!');
})
.on('beforeRequest', function (err, args) {
    console.log(args.request.body.raw);
});
};