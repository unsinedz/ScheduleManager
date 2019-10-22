var path = require('path');
var webpack = require('webpack');
module.exports = {
    context: path.resolve(__dirname, 'wwwroot', 'scripts'),
    output: {
        path: path.resolve(__dirname, 'build'),
        publicPath: '/',
        filename: 'app.js'
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                include: path.resolve(__dirname, 'wwwroot', 'scripts'),
                exclude: /node_modules/,
                use: ['babel-loader', 'eslint-loader']
            },
            {
                test: /(\.css)$/,
                include: path.resolve(__dirname, 'wwwroot', 'css'),
                use: ['style-loader', 'css-loader']
            }
        ]
    },
    devtool: 'source-map'
};