import { useEffect, useState } from "react";

import { getCategories } from "../services/assetCategoryService";

import {
    getAssets,
    createAsset,
    updateAsset,
    deleteAsset
} from "../services/assetService";

import {
    getVersions,
    createVersion,
    updateVersion,
    deleteVersion
} from "../services/assetVersionService";

function AssetDashboard() {
    const [categories, setCategories] = useState([]);
    const [assets, setAssets] = useState([]);

    const [selectedCategory, setSelectedCategory] = useState(null);
    const [selectedAsset, setSelectedAsset] = useState(null);

    const [showVersions, setShowVersions] = useState(false);

    const [name, setName] = useState("");
    const [contributor, setContributor] = useState("");
    const [projectName, setProjectName] = useState("");

    const [assetCategoryId, setAssetCategoryId] = useState(null);

    const [editingId, setEditingId] = useState(null);

    const [versions, setVersions] = useState([]);

    const [versionNumber, setVersionNumber] = useState("");
    const [releaseDate, setReleaseDate] = useState("");
    const [notes, setNotes] = useState("");

    const [editingVersionId, setEditingVersionId] = useState(null);

    useEffect(() => {
        loadCategories();
        loadAssets();
        loadVersions();
    }, []);

    const loadCategories = async () => {
        const data = await getCategories();
        setCategories(data);
    };

    const loadAssets = async () => {
        const data = await getAssets();
        setAssets(data);
    };

    const loadVersions = async () => {
        const data = await getVersions();
        setVersions(data);
    };

    const filteredAssets = selectedCategory
        ? assets.filter(
            asset =>
                asset.categoryId ===
                selectedCategory.categoryId
        )
        : assets;

    const handleEdit = () => {
        if (!selectedAsset) return;

        setEditingId(selectedAsset.assetId);

        setAssetCategoryId(
            selectedAsset.categoryId
        );

        setName(selectedAsset.name);
        setContributor(selectedAsset.contributor);
        setProjectName(selectedAsset.projectName || "");
    };

    const handleSave = async () => {
        if (!selectedCategory) {
            alert("Select a category first.");
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

        const assetData = {
            categoryId:
                editingId !== null
                    ? assetCategoryId
                    : selectedCategory.categoryId,
            name,
            contributor,
            projectName
        };

        try {
            if (editingId !== null) {
                await updateAsset(editingId, assetData);
            } else {
                await createAsset(assetData);
            }

            setEditingId(null);
            setName("");
            setContributor("");
            setProjectName("");

            await loadAssets();
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleDelete = async () => {
        if (!selectedAsset) {
            alert("Select an asset first.");
            return;
        }

        if (
            !window.confirm(
                "Deleting this asset will also delete all associated versions. Continue?"
            )
        ) {
            return;
        }

        try {
            await deleteAsset(selectedAsset.assetId);

            setSelectedAsset(null);
            setEditingId(null);

            setName("");
            setContributor("");
            setProjectName("");

            await loadAssets();
        }
        catch (error) {
            console.error(error);
        }
    };

    const filteredVersions = selectedAsset
        ? versions.filter(
            version =>
                version.assetId ===
                selectedAsset.assetId
        )
        : [];

    const handleVersionSave = async () => {
        if (!versionNumber.trim()) {
            alert("Version number is required.");
            return;
        }

        if (!releaseDate) {
            alert("Release date is required.");
            return;
        }

        const versionData = {
            assetId: selectedAsset.assetId,
            versionNumber,
            releaseDate,
            notes
        };

        try {
            if (editingVersionId !== null) {
                await updateVersion(
                    editingVersionId,
                    versionData
                );
            } else {
                await createVersion(versionData);
            }

            setEditingVersionId(null);
            setVersionNumber("");
            setReleaseDate("");
            setNotes("");

            await loadVersions();
        }
        catch (error) {
            console.error(error);
        }
    };

    if (showVersions) {
        return (
            <div
                style={{
                    display: "flex",
                    gap: "20px",
                    padding: "20px"
                }}
            >
                <div
                    style={{
                        width: "20%"
                    }}
                >
                    <h2
                        style={{
                            color: "#C97BEA"
                        }}
                    >
                        Categories
                    </h2>

                    <button>
                        All Assets
                    </button>

                    {categories.map(category => (
                        <div key={category.categoryId}>
                            <button>
                                {category.name}
                            </button>
                        </div>
                    ))}
                </div>

                <div
                    style={{
                        width: "80%"
                    }}
                >
                    <button
                        onClick={() =>
                            setShowVersions(false)
                        }
                    >
                        ← Back
                    </button>

                    <h1
                        style={{
                            color: "#C97BEA"
                        }}
                    >
                        Versions for {selectedAsset?.name}
                    </h1>

                    <hr />

                    <div>
                        <label>Version Number</label>
                        <input
                            type="text"
                            value={versionNumber}
                            onChange={(e) =>
                                setVersionNumber(e.target.value)
                            }
                        />
                    </div>

                    <div>
                        <label>Release Date</label>
                        <input
                            type="date"
                            value={releaseDate}
                            onChange={(e) =>
                                setReleaseDate(e.target.value)
                            }
                        />
                    </div>

                    <div>
                        <label>Notes</label>
                        <input
                            type="text"
                            value={notes}
                            onChange={(e) =>
                                setNotes(e.target.value)
                            }
                        />
                    </div>

                    <button onClick={handleVersionSave}>
                        {editingVersionId !== null
                            ? "Update Version"
                            : "Create Version"}
                    </button>

                    {filteredVersions.map(version => (
                        <div
                            key={version.versionId}
                            style={{
                                border: "1px solid gray",
                                padding: "10px",
                                marginBottom: "10px"
                            }}
                        >
                            <div>
                                <strong>
                                    {version.versionNumber}
                                </strong>
                            </div>

                            <div>
                                Release Date:
                                {" "}
                                {version.releaseDate}
                            </div>

                            <div>
                                Notes:
                                {" "}
                                {version.notes}
                            </div>

                            <button
                                onClick={() => {
                                    setEditingVersionId(
                                        version.versionId
                                    );

                                    setVersionNumber(
                                        version.versionNumber
                                    );

                                    setReleaseDate(
                                        version.releaseDate.split("T")[0]
                                    );

                                    setNotes(
                                        version.notes || ""
                                    );
                                }}
                            >
                                Edit
                            </button>

                            <button
                                onClick={async () => {
                                    if (
                                        !window.confirm(
                                            "Delete this version?"
                                        )
                                    ) {
                                        return;
                                    }

                                    await deleteVersion(
                                        version.versionId
                                    );

                                    await loadVersions();
                                }}
                            >
                                Delete
                            </button>
                        </div>
                    ))}
                </div>
            </div>
        );
    }

    return (
        <div
            style={{
                display: "flex",
                gap: "20px",
                padding: "20px"
            }}
        >
            {/* LEFT */}
            <div
                style={{
                    width: "20%"
                }}
            >
                <h2
                    style={{
                        color: "#C97BEA"
                    }}
                >
                    Categories
                </h2>

                <button
                    onClick={() => setSelectedCategory(null)}
                    style={{
                        backgroundColor:
                            selectedCategory === null
                                ? "#333"
                                : "#222",
                        color: "white",
                        border:
                            selectedCategory === null
                                ? "2px solid yellow"
                                : "1px solid gray",
                        width: "100%",
                        marginBottom: "10px",
                        cursor: "pointer"
                    }}
                >
                    All Assets
                </button>

                {categories.map(category => (
                    <div key={category.categoryId}>
                        <button
                            onClick={() =>
                                setSelectedCategory(
                                    selectedCategory?.categoryId ===
                                        category.categoryId
                                        ? null
                                        : category
                                )
                            }
                            style={{
                                backgroundColor:
                                    selectedCategory?.categoryId ===
                                        category.categoryId
                                        ? "#333"
                                        : "#222",
                                color: "white",
                                border:
                                    selectedCategory?.categoryId ===
                                        category.categoryId
                                        ? "2px solid yellow"
                                        : "1px solid gray",
                                width: "100%",
                                marginBottom: "5px",
                                cursor: "pointer"
                            }}
                        >
                            {category.name}
                        </button>
                    </div>
                ))}
            </div>

            {/* MIDDLE */}
            <div
                style={{
                    width: "60%"
                }}
            >
                <h2
                    style={{
                        color: "#C97BEA"
                    }}
                >
                    Assets
                </h2>

                <div
                    style={{
                        display: "flex",
                        flexWrap: "wrap",
                        gap: "20px"
                    }}
                >
                    {filteredAssets.map(asset => (
                        <div
                            key={asset.assetId}
                            onClick={() =>
                                setSelectedAsset(
                                    selectedAsset?.assetId === asset.assetId
                                        ? null
                                        : asset
                                )
                            }
                            style={{
                                border:
                                    selectedAsset?.assetId === asset.assetId
                                        ? "3px solid yellow"
                                        : "1px solid gray",
                                backgroundColor:
                                    selectedAsset?.assetId === asset.assetId
                                        ? "#333"
                                        : "#222",
                                padding: "10px",
                                borderRadius: "8px",
                                width: "250px",
                                cursor: "pointer"
                            }}
                        >
                            <img
                                src="/slimedev.jpeg"
                                alt="SlimeDev"
                                style={{
                                    width: "100%",
                                    height: "180px",
                                    objectFit: "cover",
                                    marginBottom: "10px"
                                }}
                            />

                            <div>
                                <strong>{asset.name}</strong>
                            </div>

                            <div>
                                Contributor: {asset.contributor}
                            </div>

                            <div>
                                Project: {asset.projectName}
                            </div>
                        </div>
                    ))}
                </div>
            </div>

            {/* RIGHT */}
            <div
                style={{
                    width: "20%"
                }}
            >
                <h2
                    style={{
                        color: "#C97BEA"
                    }}
                >
                    Actions
                </h2>

                <p>
                    Selected Asset:
                    {selectedAsset?.name || "None"}
                </p>

                <button
                    onClick={() => {
                        setSelectedAsset(null);
                        setEditingId(null);

                        setName("");
                        setContributor("");
                        setProjectName("");
                    }}
                >
                    Add Asset
                </button>

                <button
                    onClick={handleEdit}
                    disabled={!selectedAsset}
                >
                    Edit Asset
                </button>

                <div>
                    <label>Name</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>

                <div>
                    <label>Contributor</label>
                    <input
                        type="text"
                        value={contributor}
                        onChange={(e) => setContributor(e.target.value)}
                    />
                </div>

                <div>
                    <label>Project</label>
                    <input
                        type="text"
                        value={projectName}
                        onChange={(e) => setProjectName(e.target.value)}
                    />
                </div>

                <button onClick={handleSave}>
                    {editingId !== null
                        ? "Update Asset"
                        : "Create Asset"}
                </button>

                <button
                    onClick={handleDelete}
                    disabled={!selectedAsset}
                >
                    Delete Asset
                </button>

                <button
                    onClick={() => {
                        if (!selectedAsset) {
                            alert("Select an asset first.");
                            return;
                        }

                        setShowVersions(true);
                    }}
                >
                    Open Versions
                </button>
            </div>
        </div>


    );
}

export default AssetDashboard;