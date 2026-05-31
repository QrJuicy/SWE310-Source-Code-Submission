import axios from "axios";

const API_URL = "http://localhost:5252/api/AssetCategory";

export const getCategories = async () => {
    const response = await axios.get(API_URL);
    return response.data;
};

export const createCategory = async (category) => {
    const response = await axios.post(API_URL, category);
    return response.data;
};

export const updateCategory = async (id, category) => {
    const response = await axios.put(
        `${API_URL}/${id}`,
        category
    );

    return response.data;
};

export const deleteCategory = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
};