import { useEffect, useState } from "react";
import {
    getVersions,
    createVersion,
    updateVersion,
    deleteVersion
} from "../services/assetVersionService";

function AssetVersionPage() {
    const [versions, setVersions] = useState([]);

    const [assetId, setAssetId] = useState(1);
    const [versionNumber, setVersionNumber] = useState("");
    const [releaseDate, setReleaseDate] = useState("");
    const [notes, setNotes] = useState("");

    const [editingId, setEditingId] = useState(null);

    useEffect(() => {
        loadVersions();
    }, []);

    const loadVersions = async () => {
        const data = await getVersions();
        setVersions(data);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!assetId) {
            alert("Asset ID is required.");
            return;
        }

        if (!versionNumber.trim()) {
            alert("Version number is required.");
            return;
        }

        if (!releaseDate) {
            alert("Release date is required.");
            return;
        }

        try {
            const versionData = {
                assetId: Number(assetId),
                versionNumber,
                releaseDate,
                notes
            };

            if (editingId !== null) {
                await updateVersion(editingId, versionData);
                setEditingId(null);
            } else {
                await createVersion(versionData);
            }

            setAssetId(1);
            setVersionNumber("");
            setReleaseDate("");
            setNotes("");

            await loadVersions();
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleDelete = async (id) => {
        if (
            !window.confirm(
                "Are you sure you want to delete this version?"
            )
        ) {
            return;
        }

        try {
            await deleteVersion(id);
            await loadVersions();
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleEdit = (version) => {
        setEditingId(version.versionId);
        setAssetId(version.assetId);
        setVersionNumber(version.versionNumber);
        setReleaseDate(
            version.releaseDate.split("T")[0]
        );
        setNotes(version.notes || "");
    };

    return (
        <div>
            <h1>Asset Versions</h1>

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Asset Id:</label>
                    <input
                        type="number"
                        value={assetId}
                        onChange={(e) =>
                            setAssetId(e.target.value)
                        }
                    />
                </div>

                <div>
                    <label>Version Number:</label>
                    <input
                        type="text"
                        value={versionNumber}
                        onChange={(e) =>
                            setVersionNumber(e.target.value)
                        }
                    />
                </div>

                <div>
                    <label>Release Date:</label>
                    <input
                        type="date"
                        value={releaseDate}
                        onChange={(e) =>
                            setReleaseDate(e.target.value)
                        }
                    />
                </div>

                <div>
                    <label>Notes:</label>
                    <input
                        type="text"
                        value={notes}
                        onChange={(e) =>
                            setNotes(e.target.value)
                        }
                    />
                </div>

                <button type="submit">
                    {editingId !== null
                        ? "Update Version"
                        : "Add Version"}
                </button>
            </form>

            <ul>
                {versions.map(version => (
                    <li key={version.versionId}>
                        {version.versionNumber}
                        {" | "}
                        {version.notes}

                        <button
                            onClick={() =>
                                handleEdit(version)
                            }
                        >
                            Edit
                        </button>

                        <button
                            onClick={() =>
                                handleDelete(version.versionId)
                            }
                        >
                            Delete
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AssetVersionPage;