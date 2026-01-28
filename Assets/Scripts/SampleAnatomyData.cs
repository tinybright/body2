using UnityEngine;

namespace AnatomyViewer
{
    /// <summary>
    /// Helper class to create sample anatomy data for testing
    /// </summary>
    public static class SampleAnatomyData
    {
        /// <summary>
        /// Create a sample anatomy database with bones and muscles
        /// </summary>
        public static AnatomyDatabase CreateSampleDatabase()
        {
            AnatomyDatabase db = ScriptableObject.CreateInstance<AnatomyDatabase>();

            // Add sample bones
            AddBone(db, "Skull", "The skull is the bony structure that forms the head, protecting the brain and supporting the face.");
            AddBone(db, "Mandible", "The mandible is the lower jaw bone, the largest and strongest bone of the face.");
            AddBone(db, "Cervical Vertebrae", "Seven vertebrae in the neck region, designated C1 to C7.");
            AddBone(db, "Thoracic Vertebrae", "Twelve vertebrae in the chest region, designated T1 to T12.");
            AddBone(db, "Lumbar Vertebrae", "Five vertebrae in the lower back, designated L1 to L5.");
            AddBone(db, "Clavicle", "The collarbone connects the shoulder blade to the sternum.");
            AddBone(db, "Scapula", "The shoulder blade is a large triangular bone in the upper back.");
            AddBone(db, "Humerus", "The bone of the upper arm, extending from shoulder to elbow.");
            AddBone(db, "Radius", "One of two bones of the forearm, on the thumb side.");
            AddBone(db, "Ulna", "One of two bones of the forearm, on the pinky side.");
            AddBone(db, "Pelvis", "The pelvic girdle consists of the hip bones and sacrum.");
            AddBone(db, "Femur", "The thighbone is the longest and strongest bone in the body.");
            AddBone(db, "Patella", "The kneecap protects the knee joint.");
            AddBone(db, "Tibia", "The shinbone is the larger of the two leg bones.");
            AddBone(db, "Fibula", "The smaller bone of the lower leg, parallel to the tibia.");

            // Add sample muscles - Layer 1 (Superficial)
            AddMuscle(db, AnatomyLayer.Muscle1, "Trapezius", "Large muscle extending over the back of the neck and shoulders.");
            AddMuscle(db, AnatomyLayer.Muscle1, "Deltoid", "Shoulder muscle responsible for arm abduction.");
            AddMuscle(db, AnatomyLayer.Muscle1, "Pectoralis Major", "Large chest muscle responsible for arm movements.");
            AddMuscle(db, AnatomyLayer.Muscle1, "Latissimus Dorsi", "Broad muscle of the back that pulls the arm down and back.");

            // Add sample muscles - Layer 2
            AddMuscle(db, AnatomyLayer.Muscle2, "Infraspinatus", "Rotator cuff muscle that externally rotates the arm.");
            AddMuscle(db, AnatomyLayer.Muscle2, "Teres Minor", "Small rotator cuff muscle that assists in external rotation.");
            AddMuscle(db, AnatomyLayer.Muscle2, "Rhomboid Major", "Retracts the scapula toward the spine.");

            // Add sample muscles - Layer 3
            AddMuscle(db, AnatomyLayer.Muscle3, "Serratus Anterior", "Muscle that protracts the scapula and helps in arm elevation.");
            AddMuscle(db, AnatomyLayer.Muscle3, "Subscapularis", "Rotator cuff muscle that internally rotates the arm.");

            // Add sample muscles - Layer 4
            AddMuscle(db, AnatomyLayer.Muscle4, "Biceps Brachii", "Two-headed muscle of the upper arm that flexes the elbow.");
            AddMuscle(db, AnatomyLayer.Muscle4, "Triceps Brachii", "Three-headed muscle that extends the elbow.");
            AddMuscle(db, AnatomyLayer.Muscle4, "Brachialis", "Muscle beneath the biceps that flexes the elbow.");

            // Add sample muscles - Layer 5
            AddMuscle(db, AnatomyLayer.Muscle5, "Quadriceps Femoris", "Four-headed muscle group that extends the knee.");
            AddMuscle(db, AnatomyLayer.Muscle5, "Hamstrings", "Group of three muscles that flex the knee and extend the hip.");
            AddMuscle(db, AnatomyLayer.Muscle5, "Gastrocnemius", "Calf muscle that plantar flexes the foot.");

            // Add sample muscles - Layer 6
            AddMuscle(db, AnatomyLayer.Muscle6, "Soleus", "Muscle beneath the gastrocnemius that plantar flexes the foot.");
            AddMuscle(db, AnatomyLayer.Muscle6, "Tibialis Anterior", "Muscle that dorsiflexes and inverts the foot.");

            // Add sample muscles - Layer 7 (Deep)
            AddMuscle(db, AnatomyLayer.Muscle7, "Iliopsoas", "Deep hip flexor composed of psoas major and iliacus.");
            AddMuscle(db, AnatomyLayer.Muscle7, "Piriformis", "Deep muscle that externally rotates the hip.");

            return db;
        }

        private static void AddBone(AnatomyDatabase db, string name, string description)
        {
            db.parts.Add(new AnatomyPartData
            {
                name = name,
                description = description,
                type = AnatomyType.Bone,
                layer = AnatomyLayer.Bone
            });
        }

        private static void AddMuscle(AnatomyDatabase db, AnatomyLayer layer, string name, string description)
        {
            db.parts.Add(new AnatomyPartData
            {
                name = name,
                description = description,
                type = AnatomyType.Muscle,
                layer = layer
            });
        }
    }
}
