module.exports = {
  entry: './main.js',
  output: {
    filename: 'dist/bundle.js'
  },
  module: {
    loaders: [
      {
	test: /\.js$/,
	exclude: /(node_modules|bower_components)/,
	loader: 'babel-loader',
	query: {
	  presets: ['env']
	}
      }
    ]
  }
};
