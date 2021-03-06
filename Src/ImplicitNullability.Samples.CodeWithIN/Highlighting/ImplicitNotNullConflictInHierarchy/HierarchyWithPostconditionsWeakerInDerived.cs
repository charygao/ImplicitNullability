using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ImplicitNullability.Samples.CodeWithIN.Highlighting.ImplicitNotNullConflictInHierarchy
{
    public static class HierarchyWithPostconditionsWeakerInDerived
    {
        public interface IInterface
        {
            void ExplicitNotNullOutParameterInInterfaceCanBeNullInDerived([NotNull] out string a);
            void ImplicitNotNullOutParameterInInterfaceCanBeNullInDerived(out string a);

            [NotNull]
            string FunctionWithExplicitNotNullInInterfaceCanBeNullInDerived();

            string FunctionWithImplicitNotNullInInterfaceCanBeNullInDerived();

            [ItemNotNull]
            Task<string> TaskFunctionWithExplicitNotNullInInterfaceCanBeNullInDerived();

            Task<string> TaskFunctionWithImplicitNotNullInInterfaceCanBeNullInDerived();

            [NotNull]
            string PropertyWithExplicitNotNullInInterfaceCanBeNullInDerived { get; }

            string PropertyWithImplicitNotNullInInterfaceCanBeNullInDerived { get; }
        }

        public class Implementation : IInterface
        {
            public void ExplicitNotNullOutParameterInInterfaceCanBeNullInDerived(
                [CanBeNull] /*Expect:AnnotationConflictInHierarchy*/ out string a)
            {
                // The invalid CanBeNull does not override the NotNull:
                a = null /*Expect:AssignNullToNotNullAttribute*/;
            }

            public void ImplicitNotNullOutParameterInInterfaceCanBeNullInDerived(
                [CanBeNull] /*Expect:AnnotationConflictInHierarchy[Implicit]*/ out string a)
            {
                a = null;
            }

            [CanBeNull] /*Expect:AnnotationConflictInHierarchy*/
            public string FunctionWithExplicitNotNullInInterfaceCanBeNullInDerived()
            {
                // The invalid CanBeNull does not override the NotNull:
                return null /*Expect:AssignNullToNotNullAttribute*/;
            }

            [CanBeNull] /*Expect:AnnotationConflictInHierarchy[Implicit]*/
            public string FunctionWithImplicitNotNullInInterfaceCanBeNullInDerived()
            {
                return null;
            }

            [ItemCanBeNull] /*Expect:AnnotationConflictInHierarchy*/
            public async Task<string> TaskFunctionWithExplicitNotNullInInterfaceCanBeNullInDerived()
            {
                return await Async.CanBeNullResult<string>();
            }

            [ItemCanBeNull] /*Expect:AnnotationConflictInHierarchy[Implicit]*/
            public async Task<string> TaskFunctionWithImplicitNotNullInInterfaceCanBeNullInDerived()
            {
                return await Async.CanBeNullResult<string>();
            }

            [CanBeNull] /*Expect:AnnotationConflictInHierarchy*/
            public string PropertyWithExplicitNotNullInInterfaceCanBeNullInDerived => null /*Expect:AssignNullToNotNullAttribute*/;

            [CanBeNull] /*Expect:AnnotationConflictInHierarchy[Implicit]*/
            public string PropertyWithImplicitNotNullInInterfaceCanBeNullInDerived => null;
        }
    }
}
