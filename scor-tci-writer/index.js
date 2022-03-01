const newman = require('newman');

module.exports.runNewman = function () {
  console.log('hi');
  newman.run({
    collection: require('../Petstore.postman_collection.json'),
    reporters: 'cli,htmlextra'
}, function (err) {
	if (err) { throw err; }
    console.log('collection run complete!');
})
.on('beforeRequest', function (err, o) {
    console.log(o.item.name);
	
});
};