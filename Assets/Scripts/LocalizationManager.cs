using System.Collections.Generic;
using UnityEngine;

namespace AnatomyViewer.Localization
{
    /// <summary>
    /// Localization manager for multiple languages
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        public enum Language
        {
            English,
            Chinese
        }

        [Header("Settings")]
        public Language currentLanguage = Language.Chinese;

        private Dictionary<string, Dictionary<Language, string>> translations;

        void Awake()
        {
            InitializeTranslations();
        }

        /// <summary>
        /// Initialize translation dictionaries
        /// </summary>
        private void InitializeTranslations()
        {
            translations = new Dictionary<string, Dictionary<Language, string>>();

            // UI Labels
            AddTranslation("bone_layer", "Bone Layer", "骨骼层");
            AddTranslation("muscle_layer", "Muscle Layer", "肌肉层");
            AddTranslation("search", "Search", "搜索");
            AddTranslation("clear", "Clear", "清除");
            AddTranslation("show_all", "Show All", "显示全部");
            AddTranslation("hide_all", "Hide All", "隐藏全部");
            AddTranslation("no_results", "No results found", "未找到结果");
            AddTranslation("name", "Name", "名称");
            AddTranslation("description", "Description", "说明");
            AddTranslation("type", "Type", "类型");
            AddTranslation("layer", "Layer", "层");

            // Anatomy Types
            AddTranslation("bone", "Bone", "骨骼");
            AddTranslation("muscle", "Muscle", "肌肉");

            // Layer Names
            AddTranslation("skeletal_system", "Skeletal System", "骨骼系统");
            AddTranslation("superficial_muscles", "Superficial Muscles (Layer 1)", "表层肌肉（第1层）");
            AddTranslation("muscle_layer_2", "Muscle Layer 2", "肌肉第2层");
            AddTranslation("muscle_layer_3", "Muscle Layer 3", "肌肉第3层");
            AddTranslation("muscle_layer_4", "Muscle Layer 4", "肌肉第4层");
            AddTranslation("muscle_layer_5", "Muscle Layer 5", "肌肉第5层");
            AddTranslation("muscle_layer_6", "Muscle Layer 6", "肌肉第6层");
            AddTranslation("deep_muscles", "Deep Muscles (Layer 7)", "深层肌肉（第7层）");

            // Common Anatomy Parts (Chinese names)
            AddTranslation("skull", "Skull", "颅骨");
            AddTranslation("mandible", "Mandible", "下颌骨");
            AddTranslation("cervical_vertebrae", "Cervical Vertebrae", "颈椎");
            AddTranslation("thoracic_vertebrae", "Thoracic Vertebrae", "胸椎");
            AddTranslation("lumbar_vertebrae", "Lumbar Vertebrae", "腰椎");
            AddTranslation("clavicle", "Clavicle", "锁骨");
            AddTranslation("scapula", "Scapula", "肩胛骨");
            AddTranslation("humerus", "Humerus", "肱骨");
            AddTranslation("radius", "Radius", "桡骨");
            AddTranslation("ulna", "Ulna", "尺骨");
            AddTranslation("pelvis", "Pelvis", "骨盆");
            AddTranslation("femur", "Femur", "股骨");
            AddTranslation("patella", "Patella", "髌骨");
            AddTranslation("tibia", "Tibia", "胫骨");
            AddTranslation("fibula", "Fibula", "腓骨");

            // Muscles
            AddTranslation("trapezius", "Trapezius", "斜方肌");
            AddTranslation("deltoid", "Deltoid", "三角肌");
            AddTranslation("pectoralis_major", "Pectoralis Major", "胸大肌");
            AddTranslation("latissimus_dorsi", "Latissimus Dorsi", "背阔肌");
            AddTranslation("biceps_brachii", "Biceps Brachii", "肱二头肌");
            AddTranslation("triceps_brachii", "Triceps Brachii", "肱三头肌");
            AddTranslation("quadriceps_femoris", "Quadriceps Femoris", "股四头肌");
            AddTranslation("hamstrings", "Hamstrings", "腘绳肌");
            AddTranslation("gastrocnemius", "Gastrocnemius", "腓肠肌");

            // Instructions
            AddTranslation("rotate_instruction", "Drag to rotate", "拖动旋转");
            AddTranslation("zoom_instruction", "Pinch to zoom", "捏合缩放");
            AddTranslation("pan_instruction", "Two fingers to pan", "双指平移");
            AddTranslation("select_instruction", "Tap to select", "点击选择");
        }

        /// <summary>
        /// Add a translation entry
        /// </summary>
        private void AddTranslation(string key, string english, string chinese)
        {
            if (!translations.ContainsKey(key))
            {
                translations[key] = new Dictionary<Language, string>();
            }

            translations[key][Language.English] = english;
            translations[key][Language.Chinese] = chinese;
        }

        /// <summary>
        /// Get translated string
        /// </summary>
        public string GetString(string key)
        {
            if (translations.ContainsKey(key) && translations[key].ContainsKey(currentLanguage))
            {
                return translations[key][currentLanguage];
            }

            // Fallback to English
            if (translations.ContainsKey(key) && translations[key].ContainsKey(Language.English))
            {
                return translations[key][Language.English];
            }

            // Return key if translation not found
            return key;
        }

        /// <summary>
        /// Set current language
        /// </summary>
        public void SetLanguage(Language language)
        {
            currentLanguage = language;
        }

        /// <summary>
        /// Get layer name in current language
        /// </summary>
        public string GetLayerName(AnatomyLayer layer)
        {
            switch (layer)
            {
                case AnatomyLayer.Bone:
                    return GetString("skeletal_system");
                case AnatomyLayer.Muscle1:
                    return GetString("superficial_muscles");
                case AnatomyLayer.Muscle2:
                    return GetString("muscle_layer_2");
                case AnatomyLayer.Muscle3:
                    return GetString("muscle_layer_3");
                case AnatomyLayer.Muscle4:
                    return GetString("muscle_layer_4");
                case AnatomyLayer.Muscle5:
                    return GetString("muscle_layer_5");
                case AnatomyLayer.Muscle6:
                    return GetString("muscle_layer_6");
                case AnatomyLayer.Muscle7:
                    return GetString("deep_muscles");
                default:
                    return layer.ToString();
            }
        }
    }
}
