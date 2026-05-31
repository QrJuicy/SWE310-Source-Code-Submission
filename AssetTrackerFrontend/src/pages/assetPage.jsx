import { useEffect, useState } from "react";
import {
    getAssets,
    createAsset,
    updateAsset,
    deleteAsset
} from "../services/assetService";

function AssetPage() {
    const [assets, setAssets] = useState([]);

    const [categoryId, setCategoryId] = useState(1);
    const [name, setName] = useState("");
    const [contributor, setContributor] = useState("");
    const [projectName, setProjectName] = useState("");

    const [editingId, setEditingId] = useState(null);

    useEffect(() => {
        loadAssets();
    }, []);

    const loadAssets = async () => {
        const data = await getAssets();
        setAssets(data);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!categoryId) {
            alert("Category ID is required.");
            return;
        }

        if (!name.trim()) {
            alert("Asset name is required.");
            return;
        }

        if (!contributor.trim()) {
            alert("Contributor is required.");
            return;
        }

        try {
            const assetData = {
                categoryId: Number(categoryId),
                name,
                contributor,
                projectName
            };

            if (editingId !== null) {
                await updateAsset(editingId, assetData);
                setEditingId(null);
            } else {
                await createAsset(assetData);
            }

            setCategoryId(1);
            setName("");
            setContributor("");
            setProjectName("");

            await loadAssets();
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleDelete = async (id) => {
        if (
            !window.confirm(
                "Deleting this asset will also delete all associated versions. Continue?"
            )
        ) {
            return;
        }

        try {
            await deleteAsset(id);
            await loadAssets();
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleEdit = (asset) => {
        setEditingId(asset.assetId);
        setCategoryId(asset.categoryId);
        setName(asset.name);
        setContributor(asset.contributor);
        setProjectName(asset.projectName || "");
    };

    return (
        <div>
            <h1>Assets</h1>

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Category Id:</label>
                    <input
                        type="number"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                    />
                </div>

                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>

                <div>
                    <label>Contributor:</label>
                    <input
                        type="text"
                        value={contributor}
                        onChange={(e) => setContributor(e.target.value)}
                    />
                </div>

                <div>
                    <label>Project Name:</label>
                    <input
                        type="text"
                        value={projectName}
                        onChange={(e) => setProjectName(e.target.value)}
                    />
                </div>

                <button type="submit">
                    {editingId !== null
                        ? "Update Asset"
                        : "Add Asset"}
                </button>
            </form>

            <ul>
                {assets.map(asset => (
                    <li key={asset.assetId}>
                        {asset.name}
                        {" | "}
                        {asset.contributor}

                        <button
                            onClick={() => handleEdit(asset)}
                        >
                            Edit
                        </button>

                        <button
                            onClick={() => handleDelete(asset.assetId)}
                        >
                            Delete
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AssetPage;