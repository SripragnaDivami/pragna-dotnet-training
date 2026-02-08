CREATE TABLE IF NOT EXISTS policy_enrollments (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    policy_id INT NOT NULL,
    status VARCHAR(10) NOT NULL CHECK (status IN ('Pending', 'Approved', 'Rejected')),
    requested_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    approved_at TIMESTAMP,

    CONSTRAINT fk_enrollment_user
        FOREIGN KEY (user_id)
        REFERENCES users(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_enrollment_policy
        FOREIGN KEY (policy_id)
        REFERENCES "policy" (id)
        ON DELETE CASCADE
);
