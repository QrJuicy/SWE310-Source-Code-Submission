import { useEffect, useState } from "react";
import {
    getCategories,
    createCategory,
    updateCategory,
    deleteCategory
} from "../services/assetCategoryService";

function AssetCategoryPage() {
    const [categories, setCategories] = useState([]);
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [editingId, setEditingId] = useState(null);

    useEffect(() => {
        loadCategories();
    }, []);

    const loadCategories = async () => {
        const data = await getCategories();
        setCategories(data);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!name.trim()) {
            alert("Category name is required.");
            return;
        }

        try {
            if (editingId !== null) {
                await updateCategory(editingId, {
                    name,
                    description
                });

                setEditingId(null);
            } else {
                await createCategory({
                    name,
                    description
                });
            }

            setName("");
            setDescription("");

            await loadCategories();
        }
        catch (error) {
            console.error(error);
        }
    };

    const handleDelete = async (id) => {
        if (
            !window.confirm(
                "Are you sure you want to delete this category?"
            )
        ) {
            return;
        }

        try {
            await deleteCategory(id);
            await loadCategories();
        }
        catch (error) {
            alert("Cannot delete category because it is being used by an asset.");
            console.error(error);
        }
    };

    const handleEdit = (category) => {
        setEditingId(category.categoryId);
        setName(category.name);
        setDescription(category.description || "");
    };

    return (
        <div>
            <h1>Asset Categories</h1>

            <form onSubmit={handleSubmit}>
                <div>
                    <label>Name:</label>
                    <input
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </div>

                <div>
                    <label>Description:</label>
                    <input
                        type="text"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </div>

                <button type="submit">
                    {editingId !== null
                        ? "Update Category"
                        : "Add Category"}
                </button>
            </form>

            <ul>
                {categories.map(category => (
                    <li key={category.categoryId}>
                        {category.name}

                        <button
                            onClick={() => handleEdit(category)}
                        >
                            Edit
                        </button>

                        <button
                            onClick={() => handleDelete(category.categoryId)}
                        >
                            Delete
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default AssetCategoryPage;