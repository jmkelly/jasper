﻿using System.Linq;
using System.Threading.Tasks;
using Baseline;
using Jasper.Internals.Codegen;
using Jasper.Internals.Compilation;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Jasper.Testing.Internals.Codegen
{
    public class WriteReturnStatementTests
    {
        private readonly IGeneratedMethod theMethod = Substitute.For<IGeneratedMethod>();
        private readonly SourceWriter theWriter = new SourceWriter();
        private readonly Variable aVariable = new Variable(typeof(string), "name");

        private AsyncMode ifTheAsyncMode
        {
            set => theMethod.AsyncMode.Returns(value);
        }

        [Fact]
        public void write_for_async_task_method()
        {
            ifTheAsyncMode = AsyncMode.AsyncTask;

            theWriter.WriteReturnStatement(theMethod);

            theWriter.Code().ReadLines().Single()
                .ShouldBe("return;");
        }

        [Fact]
        public void write_for_return_task()
        {
            var expected = $"return {typeof(Task).FullName}.{nameof(Task.CompletedTask)};";

            ifTheAsyncMode = AsyncMode.ReturnCompletedTask;

            theWriter.WriteReturnStatement(theMethod);

            theWriter.Code().ReadLines().Single()
                .ShouldBe(expected);
        }

        [Fact]
        public void write_for_return_from_last_node()
        {
            var expected = $"return {typeof(Task).FullName}.{nameof(Task.CompletedTask)};";

            ifTheAsyncMode = AsyncMode.ReturnFromLastNode;

            theWriter.WriteReturnStatement(theMethod);

            theWriter.Code().ReadLines().Single()
                .ShouldBe(expected);
        }


        [Fact]
        public void write_for_variable_and_async_task_method()
        {
            ifTheAsyncMode = AsyncMode.AsyncTask;

            theWriter.WriteReturnStatement(theMethod, aVariable);

            theWriter.Code().ReadLines().Single()
                .ShouldBe("return name;");
        }

        [Fact]
        public void write_for_variable_and_return_task()
        {
            var expected = $"return {typeof(Task).FullName}.{nameof(Task.FromResult)}(name);";

            ifTheAsyncMode = AsyncMode.ReturnCompletedTask;

            theWriter.WriteReturnStatement(theMethod, aVariable);

            theWriter.Code().ReadLines().Single()
                .ShouldBe(expected);
        }

        [Fact]
        public void write_for_variable_return_from_last_node()
        {
            var expected = $"return {typeof(Task).FullName}.{nameof(Task.FromResult)}(name);";

            ifTheAsyncMode = AsyncMode.ReturnFromLastNode;

            theWriter.WriteReturnStatement(theMethod, aVariable);

            theWriter.Code().ReadLines().Single()
                .ShouldBe(expected);
        }
    }


}
