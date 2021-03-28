module.exports = {
  devServer: {
    proxy: {
      '/': {
        target: 'https://localhost:5001',
        changeOrigin: true,
      },
    },
  },
};
