﻿using System;
using System.Threading.Tasks;
using Windows.AI.MachineLearning.Preview;
using Windows.Storage;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
namespace FunWithFER.MLModels
{
    public sealed class TinyYoloModel
    {
        private LearningModelPreview learningModel;

        public static async Task<TinyYoloModel> CreateTinyYoloModel(StorageFile file)
        {
            var learningModel = await LearningModelPreview.LoadModelFromStorageFileAsync(file);

            return new TinyYoloModel { learningModel = learningModel };
        }

        public async Task<TinyYoloModelOutput> EvaluateAsync(TinyYoloModelInput input)
        {
            var output = new TinyYoloModelOutput();

            var binding = new LearningModelBindingPreview(learningModel);
            binding.Bind("image", input.image);
            binding.Bind("grid", output.grid);

            var evalResult = await learningModel.EvaluateAsync(binding, string.Empty);

            return output;
        }
    }
}
