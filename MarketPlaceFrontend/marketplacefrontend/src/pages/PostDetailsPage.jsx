import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { PostsAPI } from "../services/api";
import { CommentsAPI } from "../services/api";

export default function PostDetailsPage() {
    const { postId } = useParams();
    const navigate = useNavigate();

    const [post, setPost] = useState(null);
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState("");
    const [editingCommentId, setEditingCommentId] = useState(null);
    const [editingText, setEditingText] = useState("");
    const [error, setError] = useState("");

    async function handleDeletePost() {
        if (!window.confirm("Are you sure you want to delete this post?")) return;
        try {
            await PostsAPI.delete(postId);
            navigate("/home");
        } catch (err) {
            setError(err.message);
        }
    }

    // --- COMMENTS ---
    async function loadComments() {
        try {
            const data = await CommentsAPI.fetchByPost(postId);
            setComments(data);
        } catch (err) {
            setError(err.message);
        }
    }

    async function handleAddComment(e) {
        e.preventDefault();
        if (!newComment.trim()) return;

        try {
            await CommentsAPI.create(postId, newComment);
            setNewComment("");
            loadComments();
        } catch (err) {
            setError(err.message);
        }
    }

    async function handleEditComment(commentId) {
        try {
            await CommentsAPI.update(commentId, editingText);
            setEditingCommentId(null);
            setEditingText("");
            loadComments();
        } catch (err) {
            setError(err.message);
        }
    }

    async function handleDeleteComment(commentId) {
        if (!window.confirm("Delete this comment?")) return;

        try {
            await CommentsAPI.delete(commentId);
            loadComments();
        } catch (err) {
            setError(err.message);
        }
    }

    useEffect(() => {
        let isMounted = true;

        (async () => {
            try {
                const postData = await PostsAPI.fetchById(postId);
                if (isMounted) setPost(postData);

                const commentsData = await CommentsAPI.fetchByPost(postId);
                if (isMounted) setComments(commentsData);
            } catch (err) {
                if (isMounted) setError(err.message);
            }
        })();

        return () => {
            isMounted = false;
        };
    }, [postId]);

    if (!post) return <p style={styles.center}>Loading post...</p>;

    return (
        <main style={styles.container}>
            {error && <div style={styles.error}>{error}</div>}

            <section style={styles.card}>
                <div style={styles.headerRow}>
                    <h1>{post.title}</h1>

                    {post.isOwner && (
                        <div style={{ display: "flex", gap: "0.5rem" }}>
                            <button
                                onClick={() => navigate(`/posts/${postId}/edit`)}
                                style={styles.editButton}
                            >
                                Edit Post
                            </button>
                            <button onClick={handleDeletePost} style={styles.deleteButton}>
                                Delete Post
                            </button>
                        </div>
                    )}
                </div>

                <p style={styles.meta}>
                    {post.category} · {post.location}
                </p>

                <p>{post.description}</p>

                {post.images?.length > 0 && (
                    <div style={styles.imageGrid}>
                        {post.images.map((img, i) => (
                            <img key={i} src={img} alt="Post" style={styles.image} />
                        ))}
                    </div>
                )}
            </section>

            <section style={styles.card}>
                <h2>Comments</h2>

                <form onSubmit={handleAddComment} style={styles.commentForm}>
                    <textarea
                        value={newComment}
                        onChange={(e) => setNewComment(e.target.value)}
                        placeholder="Leave a comment..."
                        rows={3}
                        style={styles.textarea}
                    />
                    <button style={styles.button}>Post Comment</button>
                </form>

                {comments.map((comment) => (
                    <div key={comment.id} style={styles.comment}>
                        <strong>{comment.username}</strong>

                        {editingCommentId === comment.id ? (
                            <>
                                <textarea
                                    value={editingText}
                                    onChange={(e) => setEditingText(e.target.value)}
                                    style={styles.textarea}
                                />
                                <button
                                    onClick={() => handleEditComment(comment.id)}
                                    style={styles.smallButton}
                                >
                                    Save
                                </button>
                            </>
                        ) : (
                            <p>{comment.content}</p>
                        )}

                        {comment.isOwner && (
                            <div style={styles.commentActions}>
                                <button
                                    onClick={() => {
                                        setEditingCommentId(comment.id);
                                        setEditingText(comment.content);
                                    }}
                                    style={styles.linkButton}
                                >
                                    Edit
                                </button>
                                <button
                                    onClick={() => handleDeleteComment(comment.id)}
                                    style={styles.linkButton}
                                >
                                    Delete
                                </button>
                            </div>
                        )}
                    </div>
                ))}
            </section>
        </main>
    );
}

// --- STYLES (unchanged) ---
const styles = {
    container: {
        padding: "2rem",
        maxWidth: "800px",
        margin: "0 auto",
    },
    card: {
        backgroundColor: "#fff",
        padding: "1.5rem",
        borderRadius: "8px",
        marginBottom: "2rem",
        boxShadow: "0 4px 10px rgba(0,0,0,0.1)",
    },
    meta: { color: "#666", marginBottom: "1rem" },
    imageGrid: { display: "flex", gap: "0.5rem", flexWrap: "wrap" },
    image: { width: "150px", height: "150px", objectFit: "cover", borderRadius: "4px" },
    commentForm: { marginBottom: "1.5rem" },
    textarea: {
        width: "100%",
        padding: "0.5rem",
        marginBottom: "0.5rem",
        borderRadius: "4px",
        border: "1px solid #ccc",
    },
    button: {
        padding: "0.5rem 1rem",
        backgroundColor: "#007bff",
        color: "#fff",
        border: "none",
        borderRadius: "4px",
        cursor: "pointer",
    },
    smallButton: { marginTop: "0.5rem", padding: "0.25rem 0.5rem" },
    comment: { borderTop: "1px solid #ddd", paddingTop: "1rem", marginTop: "1rem" },
    commentActions: { display: "flex", gap: "0.5rem" },
    linkButton: { background: "none", border: "none", color: "#007bff", cursor: "pointer", padding: 0 },
    error: { marginBottom: "1rem", backgroundColor: "#fdecea", color: "#b00020", padding: "0.5rem", borderRadius: "4px" },
    center: { textAlign: "center", marginTop: "3rem" },
    headerRow: { display: "flex", justifyContent: "space-between", alignItems: "center", gap: "1rem" },
    editButton: { padding: "0.4rem 0.75rem", backgroundColor: "#ffc107", border: "none", borderRadius: "4px", cursor: "pointer", fontWeight: "bold" },
    deleteButton: { padding: "0.4rem 0.75rem", backgroundColor: "#dc3545", color: "#fff", border: "none", borderRadius: "4px", cursor: "pointer", fontWeight: "bold" },
};
