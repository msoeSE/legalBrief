exports.config = {
    seleniumAddress: 'http://localhost:4444/wd/hub',
    specs: ['form-validation-spec.js', 'registration-validation-spec.js'],
    //specs: ['registration-validation-spec.js'],
    framework: 'jasmine2',

    capabilities: {
        'browserName': 'chrome'
        //'maxInstances': 2, // will split your test files across 2 browser instances
        //'shardTestFiles': true,
    }, 

    baseUrl: 'http://localhost:27902',

    jasmineNodeOpts: {
        showColors: true,
        defaultTimeoutInterval: 30000
    } 
};