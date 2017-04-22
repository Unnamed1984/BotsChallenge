using AutoMapper;
using BotChallenge.BLL.Entities.DTO;
using BotChallenge.Compiler;
using BotChallenge.Compiler.Compilers;
using BotChallenge.Compiler.Compilers.Models;
using BotChallenge.Compiler.LanguageProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotChallenge.BLL.Logic
{
    public class BotsCompiler
    {
        private ILanguageProvider compProvider;
        private ICompiler compiler;
        private IMapper _mapper;

        public BotsCompiler()
        {
            compProvider = new LanguageProvider();
            compiler = compProvider.GetCompilerForLanguage(CompilerSupportedLanguages.CSharp);
        }

        public BotsCompiler(CompilerSupportedLanguages language)
        {
            compProvider = new LanguageProvider();
            compiler = compProvider.GetCompilerForLanguage(language);
        }

        private void ConfigureMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompilationResult, CompilationResultDTO>();
            });

            _mapper = config.CreateMapper();
        }

        public CompilationResultDTO CompileBot(String code)
        {
            this.ConfigureMapper();

            CompilationResult res = compiler.VerifyBotCode(code);
            return _mapper.Map<CompilationResultDTO>(res);            
        }

        public CompilationResultDTO CompileBotsCode(string[] code, int botsCount)
        {
            this.ConfigureMapper();

            CompilationResult res = compiler.CompileCode(TaskParameters.Build(botsCount), code);

            return _mapper.Map<CompilationResultDTO>(res);
        }
    }
}
