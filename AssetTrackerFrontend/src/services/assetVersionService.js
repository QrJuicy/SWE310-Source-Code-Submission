import axios from "axios";

const API_URL = "http://localhost:5252/api/AssetVersion";

export const getVersions = async () => {
    const response = await axios.get(API_URL);
    return response.data;
};

export const createVersion = async (version) => {
    const response = await axios.post(API_URL, version);
    return response.data;
};

export const updateVersion = async (id, version) => {
    const response = await axios.put(
        `${API_URL}/${id}`,
        version
    );

    return response.data;
};

export const deleteVersion = async (id) => {
    await axios.delete(`${API_URL}/${id}`);
};