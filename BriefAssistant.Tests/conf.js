exports.config = {
    seleniumAddress: 'http://localhost:4444/wd/hub',
    specs: ['form-validation-spec.js'],
    framework: 'jasmine2',

    capabilities: {
        'browserName': 'chrome'
    }, 

    baseUrl: 'http://localhost:27902',

    jasmineNodeOpts: {
        showColors: true,
        defaultTimeoutInterval: 30000
    } 
};