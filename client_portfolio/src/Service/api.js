import axios from 'axios';

const API_BASE_URL = 'https://portfolioserver-a7aw.onrender.com/api/Portfolio';

export const getRepositories = async () => {
  const response = await axios.get(`${API_BASE_URL}`);
  return response.data;
};

export const searchRepositoriesByLanguage = async (language) => {
  const response = await axios.get(`${API_BASE_URL}/searchMyRepo/${language}`);
  return response.data;
};
