import axios from 'axios';

const API_BASE_URL = 'https://portfolioserver-a7aw.onrender.com/api/Portfolio';

export const getRepositories = async () => {
  try {
    const response = await axios.get(`${API_BASE_URL}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching repositories:", error);
    throw error;
  }
};

export const searchRepositoriesByLanguage = async (language) => {
  try {
    const response = await axios.get(`${API_BASE_URL}/searchMyRepo/${language}`);
    return response.data;
  } catch (error) {
    console.error("Error searching repositories by language:", error);
    throw error;
  }
};
