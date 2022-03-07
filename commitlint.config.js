module.exports = {
    plugins: ['commitlint-plugin-jira-rules'],
    extends: ['jira'],
    rules: {
        'subject-min-length': [2, 'always', 10],
    },
    parserPreset: {
        parserOpts: {
            issuePrefixes: ['SHOOT-'],
        }
    },
};
